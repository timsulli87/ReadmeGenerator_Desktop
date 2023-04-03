# ReadmeGenerator_Desktop

<br>

## **Overview**
<br>

Desktop version of the Readme Generator app. The app allows the user to load an XML documentation file, transform it into a readme.md file, and save the output file.

<br>

## **frmMain**

Controller for the main form


### **Functions**

btn_LoadXML_Click
<ul>
<li>
Creates an open file dialog to allow user to select XML file
</li>
<li>
Parameters
<ul>
<li>Object sender</li>
<li>EventArgs) e</li>
</ul>
</li>
</ul>

btn_Generate_Click
<ul>
<li>
Calls the CreateDoc() method to read the XML file and generate the readme.md string
</li>
<li>
Parameters
<ul>
<li>Object sender</li>
<li>EventArgs) e</li>
</ul>
</li>
</ul>

btn_Save_Click
<ul>
<li>
Creates a save file dialog to allow user to save the readme.md file
</li>
<li>
Parameters
<ul>
<li>Object sender</li>
<li>EventArgs) e</li>
</ul>
</li>
</ul>

btn_Exit_Click
<ul>
<li>
Closes the app
</li>
<li>
Parameters
<ul>
<li>Object sender</li>
<li>EventArgs) e</li>
</ul>
</li>
</ul>
<br>

## **ClassObj**

Class the represents the class objects of the XML document file.            The Methods, Properties, and Events class properties are lists of class objects of their corresponding classes.


### **Properties**

Name
<ul><li>Class name</li></ul>

Summary
<ul><li>Class summary</li></ul>

Methods
<ul><li>List of methods contained in the class</li></ul>

Properties
<ul><li>List of properties contained in the class</li></ul>

Events
<ul><li>List of events contained in the class</li></ul>
<br>

## **EventObj**

Class the represents the event objects of the XML document file.


### **Properties**

Name
<ul><li>Event name</li></ul>

Summary
<ul><li>Event summary</li></ul>
