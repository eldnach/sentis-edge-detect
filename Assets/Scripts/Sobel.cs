using System;
using System.Collections;
using Unity.Sentis;
using Unity.Sentis.Layers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Sobel : MonoBehaviour
{
    [SerializeField] private BackendType backend = BackendType.GPUCompute;
    [SerializeField] private ModelAsset myModel;
    [SerializeField] private RenderTexture inputTexture;
    [SerializeField] private RawImage outputImage;

    private int w = 256;
    private int h = 256;

    private Model model;
    private Worker worker;
    private Tensor<float> inputTensor;
    private Tensor<float> outputTensor;
    private RenderTexture outputTexture;
    
    private void Start()
    {   
        inputTensor = new Tensor<float>(new TensorShape(1, 1, h, w));
        model = ModelLoader.Load(myModel);
        var graph = new FunctionalGraph();
        var input = graph.AddInput(DataType.Float, inputTensor.shape);
        var output = Functional.Forward(model, input)[0];
        model = graph.Compile(output);
        
        worker = new Worker(model, backend);
        outputTexture = new RenderTexture(w, h, 0);
        outputImage.texture = outputTexture;
    }

    private void Update()
    {
        TextureConverter.ToTensor(inputTexture, inputTensor, new TextureTransform());
        worker.Schedule(inputTensor);
        outputTensor = worker.PeekOutput() as Tensor<float>;

        TextureTransform settings = new TextureTransform().SetBroadcastChannels(false).SetDimensions(w, h, 4);
        TextureConverter.RenderToTexture(outputTensor, outputTexture, settings);
    }

    private void OnDisable()
    {
        worker.Dispose();
        inputTensor.Dispose();
        outputTensor.Dispose();
        inputTexture.Release();
        outputTexture.Release();
    }
}