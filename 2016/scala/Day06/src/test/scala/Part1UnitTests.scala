import org.scalatest._
import solution.part1.Solver

class Part1UnitTests extends FunSuite {
  test("part 1") {
    val input = List(
      "eedadn",
      "drvtee",
      "eandsr",
      "raavrd",
      "atevrs",
      "tsrnev",
      "sdttsa",
      "rasrtv",
      "nssdts",
      "ntnada",
      "svetve",
      "tesnvt",
      "vntsnd",
      "vrdear",
      "dvrsen",
      "enarar")

    val solver = new Solver(input)
    val actual = solver.solve
    assertResult("easter")(actual)
  }
}