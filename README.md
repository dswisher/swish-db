# swish-db

The intent behind swish-db is to do a few things well.
It is _not_ a general purpose database system.

The motivating use case is one where a large number of objects (3 million) are gathered
together in random key order. Once all objects have been gathered, the data is indexed and
shipped to many machines for subsequent processing, in sequential key order. The keys are
long (64-bit) integers. These files must be stored for 10+ years, so the format must be
well documented and repairable.

This leads to the following requirements and constraints:

* The database must be a single file, and storage should be relatively efficient
* The format of the objects is opaque to the DB - it is only aware of a key and sequence of bytes
* Inserts must be fast
* Enumerating objects in sequential key order must be fast
* It is acceptable to enforce a "reindex" phase after all inserts, prior to enumeration
* Must be designed for use with C# async/await
* A database will only ever be in use by a single process.


# File organization

A database file is organized as a sequence of small pages (8K by default).
There are multiple types of pages:

* The zero page, which has pointers to other pages, magic numbers, and configuration info.
* Index pages, which are used to locate objects by key
* Object pages, which are sequential series of pages used to store an object


# Code components

* `DatabaseManager` is used to open a database file (page file). This is the main entry point to begin using the library.
* An `IDatabaseFile` instance is used to read/write objects.


# Links

* [SQLite file format](https://www.sqlite.org/fileformat.html)
* [ECS 165B Spring 2011 - Database System Implementation](https://www.cs.ucdavis.edu/~green/courses/ecs165b-s11/), primarily lectures 3 and 4
* [RedBase](https://web.stanford.edu/class/cs346/2015/redbase.html), [paged file component](https://web.stanford.edu/class/cs346/2015/redbase-pf.html)

