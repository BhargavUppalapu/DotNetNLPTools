The model triained using 2507 training files, dictionary size is 3000. Best choice for cutoff is 4.0F.
Use the following code to load the model:
var predictor = new DisclaimerPredict("disclaimer.vw", "disclaimer.dict", 3000, 4.0f);