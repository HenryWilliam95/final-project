This is a brief understanding of my project, and outline of the work I intend to create, my full proposal found in the ##documents tab

#Using A Behaviour Tree to Control the Behaviours of Artificial Intelligence in a Stealth Environment

The aim of my Final Project Artefact is to test and improve my knowledge of Artificial Intelligence (AI) specifically how Behaviour Trees (BT) can be used to control an agent’s decision choices in a stealth environment.  I will be using the Unity3D Game Engine to build the project, this will allow me to quickly build and iterate the test environment, I will also be using Unity to gain access to the built-in NavMesh tool which “can be used to do spatial queries, like pathfinding and walkability tests (“Unity - Scripting API: NavMesh,” 2018).
To begin the project, I will create a small test scene within Unity to learn how to use the built-in NavMesh system.  To test my knowledge of the NavMesh I will start with a simple scene and begin increasing the complexity to test Line of Sight (LoS) to objects around the scene, and watch the agent move to set locations.  If there is time as a stretch goal, I would like to try and implement my own pathfinding algorithms.

Once the NavMesh has been set up, I will start building a simple BT to allow the agent to enact certain behaviours, such as a seek state causing the agent to move to a certain location, and a patrol state causing the agent to wander around the scene.  I have chosen to implement a BT since they are easy to expand upon if “the behaviours are written generically enough, many different agents could share not only the behaviours, but even whole trees.” (Dawe, 2014, p95). 

After implementing a basic BT, I will begin implementing a Blackboard System, this will allow each agent to have “a shared memory space which various AI components can use to store knowledge that may be of use to more than one of them” (Dill, 2014, p66).  Using the blackboard, I will be able to simulate agents talking to each other, such as sharing the player’s last known position.  The blackboard can also be used to store expensive checks such as pathfinding or LoS so the agents “can run the check once and then cache it on the blackboard” (Dill, 2014, p67).

Once I have a working BT and blackboard I will begin expanding the agent’s available behaviours, thereby improving the illusion of intelligence. I will do this by introducing decorator, sequence and selector nodes as described by Champandard and Dunstan (2014, pp78-81).  This will allow the agent to make choices based on what is currently happening around them, executing behaviours when certain conditions are met.

Sequence and selector nodes are derived from a base class called a composite node, these can be used to make “more interesting, intelligent behaviours by combining simpler behaviours together.” (Champandard and Dunstan, 2014, p78).  This will allow the agent to make decisions by traversing the tree using predefined conditions, this can be used to break the seek state down further by using a selector node, with the children behaviours being hunting player, searching last know position, and investigate.  While they all perform the basic seek state, they will behave differently depending on the conditioning, for example searching last known position will cause the agent to move towards where the player was last sighted acting cautiously, ready to run to the nearest cover, whereas the investigate state will run if a trigger has been caused such as a noise near the agent, but the agent did not see the player, then the agent will move towards the location in a confused state. 

• Champandard, A. J and Dunstan, P. (2014) “The Behaviour Tree Starter Kit” in Game AI Pro, edited by Steve Rabin. Boca Raton, FL: CRC Press 2014, pp. 73-91.

• Dawe, M. (2014) “Real-World Behaviour Trees in Script” in Game AI Pro, edited by Steve Rabin. Boca Raton, FL: CRC Press 2014, pp. 93-98

• Dill, K. (2014) “Structural Architecture – Common Tricks of the Trade” in Game AI Pro, edited by Steve Rabin. Boca Raton, FL: CRC Press 2014, pp 61-71.

• Unity – Scripting API: NavMesh. (2018) Available at https://docs.unity3d.com/ScriptReference/AI.NavMesh.html [Accessed 24th Sep. 2018]
