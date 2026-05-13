# Red Rover Code Puzzle Thoughts

## A few design Notes

### Projects
It took me longer than I care to admit to decide whether to break it out into several projects or not, 
this is a lot thinner than warrants multiple solutions, but I wanted to show seperation of concerns so I settled
on "services" for most of the work and keeping a very light console app to demonstrate my thoughts on a lean client
In a larger app, I would have more of an n tier approach in a real world situation.

### String Extensions
I tried to use string extensions for functions that mirrored existing extensions.

#### SplitOnFirst
SplitOnFirst is not how I would do this in production, It should really return a two part array and not (string, string)
given thats the expected behavior for string.split methods, this made for easier inline tests and a quicker solution 

## Transparancy of Sources / References / Admissions
I did not use AI, but i did use Google and StackOverflow for the following (as I would on the job)

- The Regex for finding parenthesis
