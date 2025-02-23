<<<<<<< HEAD
# Robotic Simulation

## Description
The Robotic Simulation project is designed to control a robot car using Unity and C#. The primary focus of this project is the `RobotController.cs` script, which handles the movement and behavior of the robot car within the simulation environment.

## Features
- **Robot Movement Control:** The `RobotController.cs` script manages the robot's wheel colliders and transforms for precise movement.
- **Obstacle Avoidance:** The robot uses sensors to detect obstacles and adjust its steering accordingly.
- **Speed Adjustment:** The script dynamically adjusts the robot's speed based on its velocity and orientation.

## Installation
1. Clone the repository:
   ```sh
   git clone https://github.com/AthulKrishnaRenjith/Robotic-simulation.git
   ```
2. Open the project in Unity.
3. Ensure you have the required dependencies installed.

## Usage
1. Attach the `RobotController` script to the robot car GameObject.
2. Configure the wheel colliders and transforms in the Unity Inspector.
3. Run the simulation to see the robot car in action.

## Demo
[Click here to watch the demo](car%20robot.mp4)

## Key Script: `RobotController.cs`
The `RobotController.cs` file contains the logic for controlling the robot car. Here are some key functions:

- `Start()`: Initializes the sensors and rigidbody.
- `FixedUpdate()`: Manages the robot's behavior each frame, including staying on the road, avoiding obstacles, and adjusting speed.
- `HandleMotor()`: Controls the motor torque and brake force.
- `HandleSteering(float direction)`: Adjusts the steering angle based on the given direction.
- `AdjustSensors(Transform sensor, float x_angle, float y_angle, float z_angle)`: Adjusts the sensors' orientation.
- `sense(Transform sensor, float dist)`: Uses raycasting to detect obstacles.
- `StayOnRoad()`: Ensures the robot stays on the road by adjusting steering.
- `AdjustSpeed()`: Dynamically adjusts the robot's speed based on its velocity and orientation.
- `AvoidObstacles()`: Adjusts steering to avoid detected obstacles.
=======
# Robotic-simulation
>>>>>>> 8e7b055ce2483062fd52ac35f356232cb7ade16d
