
# C# Band Tracking App
## Created by Charles Emrich

### Description
A simple app for managing a database of bands and their venues played.

### Installation
1. Download or clone the repository from [here](https://github.com/CharlesEmrich/band-tracker.git).
2. Using SSMS, or another program with similar functionality, the databases can be reconstructed by executing the .sql files found in Databases. In SSMS, that involves opening the file and selecting Execute Script... from the top button bar.
3. Start the app by running dnx run, or a similar local-server-starting command in your terminal of choice.
4. If debugging is necessary, the command "dnx test" is available.
5. Point your web browser to localhost:5004 to begin using the app.

### Specifications
| Behavior | Input | Output |
| - | - | - |
| User views a band. | Click bandname. | Show list of venues played by the band and option to add a venue. |
| User views a venue. | Click venue name. | Show list of bands who've played that venue and option to add one. |
|  |  |  |

### Known Bugs
There are no known bugs at this time.

#### License
This project is licensed under the MIT License.
Copyright (c) 2017 Charles Emrich
