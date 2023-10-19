/**
* <author>Christophe Roblin</author>
* <url>lifxmod.com</url>
* <credits></credits>
* <description>Removes stumps and undergrowth during bootup</description>
* <license>GNU GENERAL PUBLIC LICENSE Version 3, 29 June 2007</license>
*/

// Register your mod as an object in the game engine, important for loading and unloading a package (mod)
if (!isObject(NoStumpsNoUndergrowth))
{
    new ScriptObject(NoStumpsNoUndergrowth)
    {
    };
}


// LiFx expect each mod to be it's own unique package
package NoStumpsNoUndergrowth
{
  // Returns a string as a version, LiFx will look for this specific function to output version to new connecting players
  // Takes no parameters, is a reserved function for LiFx compatability.
  function NoStumpsNoUndergrowth::version() {
    return "v1.0.0";
  }

  // The setup method is required, and will be looked for by the framework, if it doesn't have it your mod will not execute
  // This is where you tell the framework, which hooks you use and what object types you have added, so that the framework can call your code at the appropiate time
  function NoStumpsNoUndergrowth::setup() {
    NoStumpsNoUndergrowth::writeToDumpSql();
  }
  
  function NoStumpsNoUndergrowth::writeToDumpSql(%this) {
    %file = new FileObject(){};
    %file.openForRead("sql/dump.sql");
    %foundStart = false;
    %start = "-- Start of Remove all undergrowth and stumps";
    %sql = %start @ "\nDELETE forest, forest_patch\nFROM forest, forest_patch\nWHERE forest.GeoDataID = forest_patch.GeoDataID  AND\n      (forest.TreeType = 11 or forest.Quality < 60);\n\nSET @c442 = 1;\nSET @c443 = 1;\nSET @c444 = 1;\nSET @c445 = 1;\nSET @c446 = 1;\nSET @c447 = 1;\nSET @c448 = 1;\nSET @c449 = 1;\nSET @c450 = 1;\n\nUPDATE forest_patch\nSET Version = @c442:=@c442+1\nWHERE TerID = 442;\n\nUPDATE forest_patch\nSET Version = @c443:=@c443+1\nWHERE TerID = 443;\n\nUPDATE forest_patch\nSET Version = @c444:=@c444+1\nWHERE TerID = 444;\n\nUPDATE forest_patch\nSET Version = @c445:=@c445+1\nWHERE TerID = 445;\n\nUPDATE forest_patch\nSET Version = @c446:=@c446+1\nWHERE TerID = 446;\n\nUPDATE forest_patch\nSET Version = @c447:=@c447+1\nWHERE TerID = 447;\n\nUPDATE forest_patch\nSET Version = @c448:=@c448+1\nWHERE TerID = 448;\n\nUPDATE forest_patch\nSET Version = @c449:=@c449+1\nWHERE TerID = 449;\n\nUPDATE forest_patch\nSET Version = @c450:=@c450+1\nWHERE TerID = 450;\n\nSET @c442 = (SELECT Max(Version) as max FROM forest_patch WHERE TerID = 442 ORDER BY TerID);\nSET @c443 = (SELECT Max(Version) as max FROM forest_patch WHERE TerID = 443 ORDER BY TerID);\nSET @c444 = (SELECT Max(Version) as max FROM forest_patch WHERE TerID = 444 ORDER BY TerID);\nSET @c445 = (SELECT Max(Version) as max FROM forest_patch WHERE TerID = 445 ORDER BY TerID);\nSET @c446 = (SELECT Max(Version) as max FROM forest_patch WHERE TerID = 446 ORDER BY TerID);\nSET @c447 = (SELECT Max(Version) as max FROM forest_patch WHERE TerID = 447 ORDER BY TerID);\nSET @c448 = (SELECT Max(Version) as max FROM forest_patch WHERE TerID = 448 ORDER BY TerID);\nSET @c449 = (SELECT Max(Version) as max FROM forest_patch WHERE TerID = 449 ORDER BY TerID);\nSET @c450 = (SELECT Max(Version) as max FROM forest_patch WHERE TerID = 450 ORDER BY TerID);\n\nUPDATE terrain_blocks\nSET ForestVersion = @c442\nWHERE ID = 442;\n\nUPDATE terrain_blocks\nSET ForestVersion = @c443\nWHERE ID = 443;\n\nUPDATE terrain_blocks\nSET ForestVersion = @c444\nWHERE ID = 444;\n\nUPDATE terrain_blocks\nSET ForestVersion = @c445\nWHERE ID = 445;\n\nUPDATE terrain_blocks\nSET ForestVersion = @c446\nWHERE ID = 446;\n\nUPDATE terrain_blocks\nSET ForestVersion = @c447\nWHERE ID = 447;\n\nUPDATE terrain_blocks\nSET ForestVersion = @c448\nWHERE ID = 448;\n\nUPDATE terrain_blocks\nSET ForestVersion = @c449\nWHERE ID = 449;\n\nUPDATE terrain_blocks\nSET ForestVersion = @c450\nWHERE ID = 450;";

    // Find if it exists
    while(!%file.isEOF())
    {
      switch$(%file.readLine()) {
        case %start:
          %foundStart = %true;
          break;
      } 
    }
    %file.close();

    
    if(!foundStart) // Not found append script
    {
      %file.openForAppend("sql/dump.sql");
      %file.writeLine(%sql);
      %file.close();
    }



  }
};
activatePackage(NoStumpsNoUndergrowth);
