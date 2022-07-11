# Models

If you want to add model-graphs (using UML) to the project, make sure to (also) include a format that is easily editable (PDF- or image-files alone are not sufficient!).  
Drawio is a popular tool to create such graphs.  
Another option that you have is to describe your graphs in a markdown-file using PlantUML-syntax, which our Gitlab can then interpret and render as a graphic when the file is viewed in a web-browser.

## PlantUML
[PlantUML reference](https://plantuml.com/en/)

If you want to describe UML-graphs using PlantUML, include it in a markdown-file as follows:

````
```plantuml
@startuml

// PlantUML-definitions

@enduml
```
````
