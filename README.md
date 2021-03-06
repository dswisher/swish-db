# swish-db

The purpose behind this project is to experiment with building a data storage
system entirely in C#.

# File organization

A database file is organized as a sequence of pages, the first three of which have a fixed meaning.

    +-------------------------------------+
    | ZeroPage - page size, version, etc. |
    +-------------------------------------+
    | Header 1                            |
    +-------------------------------------+
    | Header 2                            |
    +-------------------------------------+
    | Additional Pages                    |
    |    ...                              |
    +-------------------------------------+

For more details, see the [File Structure](docs/file-structure.md) page.


# Transactions

TODO


# Links

## Other Embedded Databases

* [H2 Database Engine](https://h2database.com/html/main.html) - Java - [github](https://github.com/h2database/h2database)
* [Apache Derby](https://db.apache.org/derby/index.html) - Java
* [HSQLDB](http://hsqldb.org/) - Java - [sourceforge SVN](https://sourceforge.net/p/hsqldb/svn/HEAD/tree/)
* [SQLite file format](https://www.sqlite.org/fileformat.html)
* [LMDB](https://en.wikipedia.org/wiki/Lightning_Memory-Mapped_Database) - [SO post](https://stackoverflow.com/questions/35279756/what-is-special-about-internal-design-of-lmdb) about the design -> [slides](http://schd.ws/hosted_files/buildstuff14/96/20141120-BuildStuff-Lightning.pdf) - [Java wrapper](https://github.com/lmdbjava/lmdbjava) on github
* [HaloDB](https://github.com/yahoo/HaloDB) by Yahoo - Java


## BTrees

* [BplusDotNet](http://bplusdotnet.sourceforge.net/) on Sourceforge, by Aaron Watters
    * Fork: [C# Btree](https://github.com/thoward/bsharptree) on Github, by Troy Howard
    * Fork: [BplusDotNet](https://github.com/petlof/BplusDotNet) on Github, by Peter Löfås
* [B+Tree in Java](https://github.com/andylamp/BPlusTree) on github
* [Old C# Btree](https://archive.codeplex.com/?p=sop) on codeplex


## Other Databases

* [CouchDB Btrees](http://guide.couchdb.org/draft/btree.html) - uses append-only design to handle concurrency

## Training, Tutorials, etc.

* [ECS 165B Spring 2011 - Database System Implementation](https://www.cs.ucdavis.edu/~green/courses/ecs165b-s11/), primarily lectures 3 and 4
* [RedBase](https://web.stanford.edu/class/cs346/2015/redbase.html), [paged file component](https://web.stanford.edu/class/cs346/2015/redbase-pf.html)

