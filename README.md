## What Is This? ##

A solver for the [Safecracker 50](https://www.amazon.com/Creative-Crafthouse-Safecracker-Difficult-Puzzles/dp/B08GCTFKV2) puzzle.

## The Algorithm ##

We're just brute-forcing, honestly. There are only 65536 possibilities (four movable wheels with 16 positions, `16^4 == 65536`), and we can return early if we find our answer, as there's only one.

There are five rings:
- The first ring has 16 outer numbers and 16 inner numbers. It is also immovable, unlike the rest
- The second, third, and fourth rings have 8 outer numbers and 16 inner numbers each.
- The fifth ring has 8 outer numbers and 0 inner numbers.

Each ring other than the first can rotate to one of 16 positions. Even indices on each of these rings show the outer number at that index of the current ring, and odd indices show the inner number at that index of the previous ring. The puzzle is solved when the sum of all 16 columns equals 50.

We will recursively search all possibilities, trimming a branch if any column sum exceeds 50. 

## Disclaimer ##

This is the second F# code I have ever written in my life, I have no real idea what I'm doing. I just wanted to learn something on a quiet day. :)

## Running It ##

`dotnet fsi safecracker.fsx`