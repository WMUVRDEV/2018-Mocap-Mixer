public var target: Transform;
function Update() {
	// Rotate the camera every frame so it keeps looking at the target 
	transform.LookAt(target);
}