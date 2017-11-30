Controls:
	- Movement:
		WASD/Arrow keys for directional movement
		Space to jump
		'>' to grow
		'<' to shrink

Current Features:
	- Character Movement/Jumping
		- Feet change when character is in the air/grounded but are static otherwise
		- Potential Issue:		- If a character hits the side of a wall they can "stick" to it by holding down that direction key until they let go
	- Character Switching
		- BOTH characters are temp. frozen until the camera reaches its new target
	- Characters can scale
	- Characters can push each other (i.e. the 'sleeping' character can be pushed by the 'awake' character by using that characters ability)
		Potential Issue (?): 	- When the wide character tries to push it is pushed in the opposite direction. So instead of pushing the tall character
								1 unit the tall character is pushed 0.5 units to the right and the wide character is pushed 0.5 units to the left.

Features To Add: 
	- Animated feet
	- Detecting when a character "dies" (currently nothing happens when a player falls out of the map) - done
	- Backgrounds/Better ground art (NOTE: 	if the ground sprites are changed then their scale is going to be completely different. If youre going to
											make new ground art, make sure it is 512x512 pixels and in the import settings set it to 400 pixels per unit
											Otherwise the scale will be completely wrong.)

When Building A Level:
	- Make sure all objects that you want the characters to be able to stand on are on the "Ground" layer
	- If youre using the ButtonTrigger script make sure it has a collider marked as a trigger on the same object, use/modify the TriggerObject
	  script to define behavior for what should happen when the button is triggered (or feel free to make your own, just keep it orgranized)
