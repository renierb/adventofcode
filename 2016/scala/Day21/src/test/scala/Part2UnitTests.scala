import org.scalatest._
import part2.Solver

class Part2UnitTests extends FunSuite {
  test("part 2: rotate based on position of letter b") {
    val input = Stream("rotate based on position of letter b")
    val actual = new Solver("ecabd", input).solve
    assertResult("abdec")(actual)
  }

  test("part 2: rotate based on position of letter d") {
    val input = Stream("rotate based on position of letter d")
    val actual = new Solver("decab", input).solve
    assertResult("ecabd")(actual)
  }

  test("part 2: un-scramble 'decab' from example") {
    val operations =
      s"""
         |swap position 4 with position 0
         |swap letter d with letter b
         |reverse positions 0 through 4
         |rotate left 1 step
         |move position 1 to position 4
         |move position 3 to position 0
         |rotate based on position of letter b
         |rotate based on position of letter d
      """.stripMargin.split("\r\n").map(_.trim).filter(_.nonEmpty).toStream

    val actual = new Solver("decab", operations.reverse).solve
    assertResult("abcde")(actual)
  }
}
