import org.scalatest._
import part1.{InputParser, Solver}

class Part1UnitTests extends FunSuite {
  test("part 1: example") {
    val input =
      """
        |Disc #1 has 5 positions; at time=0, it is at position 4.
        |Disc #2 has 2 positions; at time=0, it is at position 1.
      """.stripMargin.trim

    val solver = new Solver(InputParser(input))
    val actual = solver.solve

    assertResult(5)(actual)
  }

  test("part 1: example with extra disc") {
    val input =
      """
        |Disc #1 has 5 positions; at time=0, it is at position 4.
        |Disc #2 has 2 positions; at time=0, it is at position 1.
        |Disc #3 has 5 positions; at time=0, it is at position 2.
      """.stripMargin.trim

    val solver = new Solver(InputParser(input))
    val actual = solver.solve

    assertResult(5)(actual)
  }

  test("part 1: answer") {
    val input =
      """
        |Disc #1 has 13 positions; at time=0, it is at position 10.
        |Disc #2 has 17 positions; at time=0, it is at position 15.
        |Disc #3 has 19 positions; at time=0, it is at position 17.
        |Disc #4 has 7 positions; at time=0, it is at position 1.
        |Disc #5 has 5 positions; at time=0, it is at position 0.
        |Disc #6 has 3 positions; at time=0, it is at position 1.
      """.stripMargin.trim

    val solver = new Solver(InputParser(input))
    val actual = solver.solve

    assertResult(203660)(actual)
  }
}
