package part2

import part1._

class Solver(input: Stream[String]) extends part1.Solver(input) {

  Registers.reset()
  Registers("a", 12)
}
