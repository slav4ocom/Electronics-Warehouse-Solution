# Electronics Warehouse Solution

This is entire C# solution, containing several projects.

Now Models and database migrations are moved to another shared project <b>CommonModels</b>
This is convenient as, gives ability to shitch from one database to another, and also share Models to several projects. This is accomplished by referencing <b>CommonModels</b> project from this that uses it.