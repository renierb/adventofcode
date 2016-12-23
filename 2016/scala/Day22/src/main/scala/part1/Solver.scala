package part1

import part1.InputParser.Node

class Solver(input: List[Node]) {

  def solve: Int = {
    val total = for {
      nodeA <- input
      nodeB <- input
      if nodeA.used > 0 && nodeA != nodeB && nodeA.used <= nodeB.avail
    } yield 1

    total.sum
  }
}
