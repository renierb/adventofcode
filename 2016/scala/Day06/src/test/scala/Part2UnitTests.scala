import org.scalatest._
import solution.part2.Solver

class Part2UnitTests extends FunSuite {
  test("part 2") {
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
    assertResult("advent")(actual)
  }
}