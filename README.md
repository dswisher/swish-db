# swish-db

The purpose behind this project is to experiment with building a data storage
system entirely in C#.

# File organization

A database file is organized as a sequence of small blocks (8K by default).


# Code components

* `BlockStore` breaks a file (stream) up into small blocks


# Links

* [SQLite file format](https://www.sqlite.org/fileformat.html)
* [ECS 165B Spring 2011 - Database System Implementation](https://www.cs.ucdavis.edu/~green/courses/ecs165b-s11/), primarily lectures 3 and 4
* [RedBase](https://web.stanford.edu/class/cs346/2015/redbase.html), [paged file component](https://web.stanford.edu/class/cs346/2015/redbase-pf.html)

