# Tensor Jam with ML-Agents 🤖
Made with Unity 2018.2.3f1 and [ML-Agents Beta 0.5.0a](https://github.com/Unity-Technologies/ml-agents/releases/tag/0.5.0a). Check out the original [Medium post](https://medium.com/tensorflow/tf-jam-shooting-hoops-with-machine-learning-7a96e1236c32) by Abe Haskins that inspired this.

## Getting started
### Installing ML Agents and Tensorflow
1. First download [ML-Agents Beta 0.5.0a](https://github.com/Unity-Technologies/ml-agents/releases/tag/0.5.0a) and import the ML-Agents folder (.../ml-agents-0.5.0a/UnitySDK/Assets/) into the projects asset folder.
2. Then install the [TFSharpPlugin unitypackage](https://s3.amazonaws.com/unity-ml-agents/0.5/TFSharpPlugin.unitypackage). 
3. Make sure you have all the necessary python dependencies installed. Find more information in the [installation docs](https://github.com/Unity-Technologies/ml-agents/blob/master/docs/Installation.md) of ML-Agents.

If you need more guidance check out the [basic guide](https://github.com/Unity-Technologies/ml-agents/blob/master/docs/Basic-Guide.md).

### Setting up the scene
After importing the packages we have to make sure everything is setup correctly. Make sure the agent is set to the following settings:

![Scene Settings for Agents](https://github.com/Sebastian-Schuchmann/tf-jam/blob/master/readmeAssets/Bildschirmfoto%202018-10-30%20um%2015.04.54.png?raw=true)

You can find those settings in the scene explorer under PlayerCollection/Player/BallSpawner(Agent).

## Using the pretrained model
I included a pretrained model in the Assets/ML-Model folder. I used my MacBook Pro to train this model for 7 hours and it pretty much hits the court everytime. To try it out, just drag the "editor_Academy_tfhoop-execute4-0" file to the "Graph Model" parameter in the brain.

![Importing model to the brain](https://github.com/Sebastian-Schuchmann/tf-jam/blob/master/readmeAssets/Bildschirmfoto%202018-10-30%20um%2015.05.40.png?raw=true)





