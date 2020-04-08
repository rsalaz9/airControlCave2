Readme:

Collisions:
The plane collisions are necessary for the game as it determines the win and lose scenario for the player. Additionally, if the player manages to take off one plane successfully, he will score a point. Which is why we used collision with the invisible object for score management.
The birds not colliding with the planes is the logical outcome in a real scenario. 

Lights:
As the game takes place during sunset, light on the plane is needed to make the plane more visible. It is also important so that the player can know the objectives by the light too. 
Street lights are added to enhance the light in the game environment. 
When the player mistakenly collides two planes, the game is over and as an alert for that, the red light was added.

Sounds:
The background sound resembles the air-control tower to give the player a feel of reality. Take off sound will assist the player to know that the plane is taking off and make future decisions based on that. When the two planes collide, a sound is added to let the player know about the collision.
Textures:
Textures were added to enhance a real-world environmental feeling for players (e.g. the buildings, windows, etc).

Animations
Animations were added inside the control tower as all the action happens outside it, so the purpose was to make the player feel like he/she is inside a real control tower working with other people and living real experiences.
The woman sitting in the control tower that is typing - character and animation imported from Mixamo resources, looks like she is helping the player coordinate the airplanes.(Carla)
The changes of the desktops’  interfaces from the right computer - animation that loops until the game is over (the text disappears once a few seconds making it look like the woman is working on it).(Ruby)
The flickering of the red light which appears when the game is over - animation triggered once 2 planes collide in order to alert the player about the gravity of the situation.(Nafiul)


AI Techniques	
Birds flock - uses a multi-agent system AI technique, namely flocking. Having a core Flock element in the scene, the birds are generated within it and this flock element groups and coordinates them using 3 behavioral elements: cohesion, alignment, and avoidance. Creating 3 menu items for these behaviors to test them, they were then combined into a single behavioral object which sorts them by priority. Then this last element is assigned to the flock element. The birds were added for a better enhancement of a real-world environment, moreover, the animations they create make the scene more dynamic.  (Carla) 
Parked Planes Push Back Position: Each parked plane makes a decision on which position it should push back. For one plane, it has four pushback position (Push A, Push B, Push C, Push D). The plane uses a pathfinding method taking distance into account. It calculates the Euclidean distance from it’s position to each push back position and selects the shortest distance. It then goes to the position which is the shortest.  (Nafiul)
In order to generate landing planes to make the game more challenging a tracker is in place to track plane positions as well as player movements. For example if there are a small number of planes at gates this will make more planes land. Also if there are planes already in the air this will limit the number of planes that are generated. Also, in order to motivate the player to move the planes a warning is generated for planes that have been at the gate for too long. This feature was accomplished by tracking the time each plane has been at the gate.	