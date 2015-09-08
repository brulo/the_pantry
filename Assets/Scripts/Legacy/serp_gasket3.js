#pragma strict
import System.Collections.Generic; // to use List

var thePrefab : GameObject;        // the prefab we copy
var copies : List.< GameObject >;  // where we store our copies
var locs : List.< Vector3 >;       // where we store locations

var sc : float;                    // scales space between iterations

var explosions : List.< AudioClip >;
var revExplosions : List.< AudioClip >;

private var resetting;
private var inPlace;
private var interruptReset;

// Main Functions
function Start(){
	sc = 13.5;
	// sierpinski(transform.position + Vector3(0,0,0), transform.position + Vector3(2.0*sc,0,0), 
	// 		   transform.position + Vector3(1.0*sc,0,1.73205*sc), transform.position + Vector3(1.0*sc,1.73205*sc,0.57735*sc), 5, 1.0);
	sierpinski(transform.position + Vector3(-1.0*sc,0,-0.57735*sc), transform.position + Vector3(sc,0,-0.577357*sc), 
			   transform.position + Vector3(0,0,1.1547*sc), transform.position + Vector3(0,1.7320508074*sc,0), 5, 1.0);
			  
	for(var x=0; x<copies.Count; x++){ 
		copies[x].AddComponent.<Rigidbody>();
		copies[x].GetComponent.<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
		copies[x].BroadcastMessage("setMute",true);
	}
	//for(var i=0;i<3;i++){
	//	var pubup : AudioClip;
	//	explosions.Add(pubup);
	//}
	inPlace=true;
}

function Update(){ }

// Utility Functions
function sierpinski(a : Vector3, b : Vector3, c : Vector3, d : Vector3, n : int, s : float){
	if(n>0){
		// store locations and instantiate copies
		if(!inList(a,locs)){
			locs.Add(a); 
			copies.Add( GameObject.Instantiate( thePrefab, a, Quaternion.identity ) );
		}
		if(!inList(b,locs)){
			locs.Add(b); 
			copies.Add( GameObject.Instantiate( thePrefab, b, Quaternion.identity ) );
		}
		if(!inList(c,locs)){
			locs.Add(c); 
			copies.Add( GameObject.Instantiate( thePrefab, c, Quaternion.identity ) );
		}
		if(!inList(d,locs)){
			locs.Add(d);
			copies.Add( GameObject.Instantiate( thePrefab, d, Quaternion.identity ) ); 
		}
		// find midpoints
		var e = Vector3.Lerp( a, b, 0.5 ); var f = Vector3.Lerp( a, c, 0.5 );
		var g = Vector3.Lerp( a, d, 0.5 ); var h = Vector3.Lerp( b, c, 0.5 );
		var i = Vector3.Lerp( b, d, 0.5 ); var j = Vector3.Lerp( c, d, 0.5 );
		// recurse 
		sierpinski( a,g,f,e, n-1, s*0.666666666 );
		sierpinski( e,h,b,i, n-1, s*0.666666666 );
		sierpinski( c,h,f,j, n-1, s*0.666666666 );
		sierpinski( d,g,i,j, n-1, s*0.666666666 );
	}
}

function inList(checkVec : Vector3, listy : List.<Vector3>){
	for(var i=0;i<listy.Count;i++){
		if(checkVec==listy[i])
			return true;
	}
	return false;
}

function explode(){
	inPlace=false;
	interruptReset=true;
	for(var z=0; z<copies.Count; z++){
		copies[z].GetComponent.<Rigidbody>().constraints = RigidbodyConstraints.None;
		copies[z].GetComponent.<Rigidbody>().AddExplosionForce(Random.Range( 600.0, 1000.0 ), 
											 transform.position + Vector3(0,0.8660254037*sc,0), 1000, 5.0);
		copies[z].BroadcastMessage("setMute",true);
	}
	if(explosions.Count>0){
		GetComponent.<AudioSource>().Stop();
		GetComponent.<AudioSource>().PlayOneShot(explosions[Random.Range(0,explosions.Count)],3.0);
	}
	for(var i=0;i<5;i++)
		yield;
	for(var j=0; j<copies.Count; j++)
		copies[j].BroadcastMessage("setMute",false);
}

function moveObject (rbody : Rigidbody, thisTransform : Transform, startPos : Vector3, endPos : Vector3, time : float) {
    var i = 0.0;
    var rate = 1.0/time;
    var startRot = thisTransform.rotation;
    while (i < 1.0) {
    	if(interruptReset){
    		resetting=false;
    		return;
    	}
        i += Time.deltaTime * rate;
        thisTransform.position = Vector3.Lerp(startPos, endPos, i);
        thisTransform.rotation = Quaternion.Slerp(startRot, Quaternion.identity, i);
        yield;
    }
    thisTransform.position = endPos;
    thisTransform.rotation = Quaternion.identity;
	rbody.constraints = RigidbodyConstraints.FreezeAll;
    resetting=false;
    inPlace=true;
}

function reset(){
	if(!resetting&&!inPlace){
		resetting=true;
		interruptReset=false;
		for(var z=0; z<locs.Count; z++){
			moveObject(copies[z].GetComponent.<Rigidbody>(), copies[z].transform, copies[z].transform.position, locs[z], 1.2);
			//copies[z].transform.rotation = Quaternion.identity;
			//copies[z].rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		}
		if(revExplosions.Count>0){
			GetComponent.<AudioSource>().Stop();
			GetComponent.<AudioSource>().PlayOneShot(revExplosions[Random.Range(0,revExplosions.Count)],3.0);
		}
	}
}

/*
// scale the copies
for(var z=1; z<5; z++){
	//copies[copies.Count-z].transform.localScale = Vector3(s-1,s-1,s-1);
}
*/