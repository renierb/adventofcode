import org.scalatest._
import part1.InputParser

class Part1UnitTests extends FunSuite {
  test("part 1") {
    val input =
      """
        |/dev/grid/node-x0-y0   10T    8T     2T   80%
        |/dev/grid/node-x0-y1   11T    6T     5T   54%
        |/dev/grid/node-x0-y2   32T   28T     4T   87%
        |/dev/grid/node-x1-y0    9T    7T     2T   77%
        |/dev/grid/node-x1-y1    8T    0T     8T    0%
        |/dev/grid/node-x1-y2   11T    7T     4T   63%
        |/dev/grid/node-x2-y0   10T    6T     4T   60%
        |/dev/grid/node-x2-y1    9T    8T     1T   88%
        |/dev/grid/node-x2-y2    9T    6T     3T   66%
      """.stripMargin

    val inputNodes = InputParser(input)
    assertResult(3)(inputNodes.count(_.x == 0))
    assertResult(3)(inputNodes.count(_.x == 1))
    assertResult(3)(inputNodes.count(_.x == 2))
    assertResult(3)(inputNodes.count(_.y == 0))
    assertResult(3)(inputNodes.count(_.y == 1))
    assertResult(3)(inputNodes.count(_.y == 2))
    assertResult(9)(inputNodes.length)
  }
}
