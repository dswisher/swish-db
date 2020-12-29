# File Structure

A database file consists of a number of pages, each has a size that is a power of 2.
The first three pages have a fixed content, the remainder can vary.

## General Page Structure

Each page begins with a page-start byte and ends with a checksum and page-end byte.
The start/end bytes have fixed values, and are used along with the checksum to detect file corruption or code bugs.

Each page has a code indicating the page type.

    +------------+-----------------------+
    |       0x00 | 1-byte Start Byte     |
    +------------+-----------------------+
    |       0x01 | 2-byte Page Type      |
    +------------+-----------------------+
    |           Page Content             |
    +------------+-----------------------+
    | end - 0x05 | 4-byte CRC32 Checksum |
    +------------+-----------------------+
    | end - 0x01 | 1-byte End Byte       |
    +------------+-----------------------+

## Zero Page

The first page, page zero, is immutable and contains the page size, version, and any
other settings that are determined at DB creation. It is special, in that it is always
read/written using the minimum page size. Page 1 still starts at the full page size
offset, so there are unused bytes at the end of the zero page.


## Page 1 and 2

TODO - what to call them?

