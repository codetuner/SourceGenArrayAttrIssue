
This sample reproduces an issue that AttributeData's ToString() method returns non-compilable string when the attribute has a param array as is the case for the [DeniedValues] data annotation attribute.

Download, compile and run. You should get "Hello World!" on the console.
(The solution contains a source generator project, you may have to restart Visual Studio after (re)compiling the CloneSourceGenerator project.)

Now uncomment the [DeniedValues] attribute declaration in the Person class in the SourceGen.SampleDomain project.
Try to compile: compilation now fails.
The attribute:
  [DeniedValues(0, 666)]
Was represented by:
  [System.ComponentModel.DataAnnotations.DeniedValuesAttribute({0, 666})]
Which, due to the curly braces, does not compile.
