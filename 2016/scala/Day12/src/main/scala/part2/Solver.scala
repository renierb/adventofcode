package part2

import part1._

class Solver(input: Stream[String]) extends part1.Solver(input) {

  override def solve: Int = {
    Registers.reset()
    Registers("c", 1)
    super.solve
  }
}
