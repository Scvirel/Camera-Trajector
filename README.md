# Camera-Trajector
This demo project demonstrates some basic tool for camera trajectory recording and playing.

# Scenes:
- <b>Bootstrap</b> - Entry game scene for some basic setups and loading basic scenes.
- <b>EventSystem</b> - Background scene with <b>EventSystem</b> and <b>StandaloneInputModule</b> components.
- <b>MainCamera</b> - Additive scene with game camera.
- <b>SunLight</b> - Background scene with <b>Directional Light.</b>
- <b>RecordingRoom</b> - Scene for camera trajectory recording (Play|Stop).
- <b>RecordingOverviewRoom</b> - Scene for overview recorded trajectory contains <b>RecordingEntry ScrollView</b> with clicable <b>PLAY</b> button for translating recorded content.

# Runtime components:
- <b>GameBootstraper</b> - Execute some basic staff and setup game. After load basic scenes (<b>MainCamera</b>,<b>EventSystem</b>,<b>SunLight</b>) and user handled scene <b>RecordingRoom</b>.
- <b>CameraRotator</b> - Rotate camera depends on user input.
- <b>CameraMovemenetRecorder</b> - <b>Begin|Complete</b> recording Camera trajectory.
- <b>CameraTrajectoryRepeaterer</b> - Translate user selected camera movement trajectory.</b>

# Editor components:
- <b>PrefsCleaner</b> - Cleanup <b>PlayerPrefs</b> and <b>EditorPrefs</b> storage (CustomEditor -> Clear Prefs).
- <b>TrajectoryEditorMenu</b> - Open some <b>Editor Window</b> for Camera movement trajectory visualizing and display gizmo trajectory inside curently opened scene after any selected (CustomEditor -> Trajectory Menu).
