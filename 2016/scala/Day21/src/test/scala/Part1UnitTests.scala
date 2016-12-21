import org.scalatest._
import part1.Solver

class Part1UnitTests extends FunSuite {
  test("part 1: swap position 4 with position 0") {
    val input = Stream("swap position 4 with position 0")
    val actual = new Solver("abcde", input).solve
    assertResult("ebcda")(actual)
  }

  test("part 1: swap letter d with letter b") {
    val input = Stream("swap letter d with letter b")
    val actual = new Solver("ebcda", input).solve
    assertResult("edcba")(actual)
  }

  test("part 1: reverse positions 0 through 4") {
    val input = Stream("reverse positions 0 through 4")
    val actual = new Solver("edcba", input).solve
    assertResult("abcde")(actual)
  }

  test("part 1: rotate left 1 step") {
    val input = Stream("rotate left 1 step")
    val actual = new Solver("abcde", input).solve
    assertResult("bcdea")(actual)
  }

  test("part 1: move position 1 to position 4") {
    val input = Stream("move position 1 to position 4")
    val actual = new Solver("bcdea", input).solve
    assertResult("bdeac")(actual)
  }

  test("part 1: move position 3 to position 0") {
    val input = Stream("move position 3 to position 0")
    val actual = new Solver("bdeac", input).solve
    assertResult("abdec")(actual)
  }

  test("part 1: rotate based on position of letter b") {
    val input = Stream("rotate based on position of letter b")
    val actual = new Solver("abdec", input).solve
    assertResult("ecabd")(actual)
  }

  test("part 1: rotate based on position of letter d") {
    val input = Stream("rotate based on position of letter d")
    val actual = new Solver("ecabd", input).solve
    assertResult("decab")(actual)
  }
}
