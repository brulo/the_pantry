// #pragma strict

// var ballPrefab : GameObject; 
// var balls : GameObject[];
// var degrees = 0.0;
// var rate = 0.0;
// var rads = 0.0;
// var numBalls = 0;
// var spacing = 0;

// function Start() {
// 	rate = 1;
// 	numBalls = 100;
// 	spacing = 7;
//     balls = new GameObject[numBalls];
// 	for(var i=0; i<balls.length; i++){
//     	var newBall : GameObject = GameObject.Instantiate(ballPrefab,Vector3(i-5,1,0),Quaternion.identity);
//     	newBall.transform.Rotate(0,0,180);
//     	var newColor = new HSBColor((i*1.0)/balls.length,1,1);
//     	newBall.SendMessage("setColor",HSBColor.ToColor(newColor));
//     	//Debug.Log((i*1.0)/balls.length);
//    		balls[i] = newBall;
//    	}
// }

// function Update () {
// 	degrees += (Time.deltaTime * rate);
// 	degrees = degrees % 360.0;
// 	rads = degrees * (Mathf.PI/180.0);
	
// 	for(var i=balls.length-1; i>=0; i--){
// 		if(i<numBalls){
// 			balls[i].transform.position = Vector3(Mathf.Sin(rads*(i+1))*spacing,Mathf.Cos(rads*(i+1))*spacing,numBalls-i);
// 			balls[i].transform.Rotate(0, 0, Time.deltaTime*rate*(i+1)*-1);
// 			//balls[i].transform.Rotate(0, 0, Time.deltaTime*rate*(i-2));
// 		}else{
// 			balls[i].active = false;
// 		}
// 	}
// }

