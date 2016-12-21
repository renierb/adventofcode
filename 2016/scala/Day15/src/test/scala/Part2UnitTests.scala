import org.scalatest._
import part1.{InputParser, Solver}

class Part2UnitTests extends FunSuite {
  test("part 2: answer") {
    val input =
      """
        |Disc #1 has 13 positions; at time=0, it is at position 10.
        |Disc #2 has 17 positions; at time=0, it is at position 15.
        |Disc #3 has 19 positions; at time=0, it is at position 17.
        |Disc #4 has 7 positions; at time=0, it is at position 1.
        |Disc #5 has 5 positions; at time=0, it is at position 0.
        |Disc #6 has 3 positions; at time=0, it is at position 1.
        |Disc #7 has 11 positions; at time=0, it is at position 0.
      """.stripMargin.trim

    val solver = new Solver(InputParser(input))
    val actual = solver.solve

    assertResult(2408135)(actual)
  }
}