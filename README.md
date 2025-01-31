# Sentis - Edge Detection Filter

Edge Detection (Sobel) filter created in pytorch, exported using .ONNX and executed in Unity via the Sentis API.

The "Sobel.cs" script loads the model and converts a RenderTexture into a Tensor, to be used as the model's input. The model's output is then converted back into a RenderTexture.

<p align="center">
  <img width="100%" src="https://github.com/eldnach/sentis-edge-detect/blob/main/.github/images/shrine.gif?raw=true" alt="shrine">
</p>

<p align="center">
  <img width="100%" src="https://github.com/eldnach/sentis-edge-detect/blob/main/.github/images/bridge.gif?raw=true" alt="bridge">
</p>

Model execution is timed at ~0.22ms on an RTX 3080 (Ti) Mobile:

<p align="center">
  <img width="100%" src="https://github.com/eldnach/sentis-edge-detect/blob/main/.github/images/sentis.png?raw=true" alt="nsight-sentis">
</p>

## Requirements
- Unity 6.1
- Sentis (version 2.1.1, obtained from the Unity Package Manager)
