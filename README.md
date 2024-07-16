# Project Overview
This project was built during my Diploma Thesis, at the department of Electrical and Computer Engineering, University of Patras.
Its purpose is to assist students, by tracking the connections they make during lab exercises, at the Electric Machines laboratory of the department.

<p align="center">
  
| <img src="https://github.com/user-attachments/assets/1d2987dc-b66d-4938-8f46-685ce53abfdc" width="80%"> |
| :---: |
| View from inside the application|
</p>


Each cable's position in space is detected using [OpenCV ArUco marker detection](https://docs.opencv.org/4.x/d5/dae/tutorial_aruco_detection.html). 
The positions of the eletrical components that the cables connect (switches, DC motor etc.), are determined by manually placing digital copies of the real components, on top of the real ones.

Cable Position Tracking          | Component placement
:-------------------------:|:-------------------------:
<img src = "https://github.com/user-attachments/assets/3b32875b-4e01-4114-874e-8ce9c58837f5">  |  <img width = "500px" src = "https://github.com/user-attachments/assets/4d929372-60a7-4d38-92fe-228d59c984ee">

The project was built with Unity version 2021.3.37f1, using Microsoft's [Mixed Reality Toolkit](https://github.com/microsoft/MixedRealityToolkit-Unity) version 2.8.3.






