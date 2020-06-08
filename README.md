File guide:

BoardDatabase: the class that contains all the board data. it contains arrays that are used to move units and spawn minerals, enemies, ect..
it also has some key methods like moveunit() 

quickaccesscords: a small script that is used to store the index position of units within the board array without going through the board array and checking each
cell.(right now it is also used to set the position of units but its temporary and will be moved to a proper class)

placeontile: used to spawn random decorational props on tiles. this will be moved to BoardDatabase soon

Unit_Movement: a script attached to the main camera that is used to check the conditions and see if it is possible to move a unit
to a tile (most commonly refered to as desx [destination x] and desy [destination y]) it also commands BoardDatabase to move a unit
