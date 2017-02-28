# 368_3_ConwayGameOfLife Conway's Game Of Life

Implement Conway's Game of Life as a console app. See  https://en.wikipedia.org/wiki/Conway's_Game_of_Life for details.

Allow the user to select from 5 Oscillator configurations as shown on Wikipedia

1. Blinker (period 2)
1. Toad (period 2)	
1. Beacon (period 2)	
1. Pulsar (period 3)	
1. Pentadecathlon (period 15)	

transition from one state to another is a user option to either
1. automate at a generation per 500 ms
2. based on user hitting a key

## Note

`Console.clear()` will clear the screen so that the patterns may appear as smooth transition

`Thread.sleep` will pause the game if automation is selected.

Use best practices and clean code

[![Build Status](https://travis-ci.org/MCO368-1/hw3-YSaxon.svg?branch=master)](https://travis-ci.org/MCO368-1/hw3-YSaxon)
