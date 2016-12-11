import org.scalatest._
import part1.Solver

class Part1UnitTests extends FunSuite {
  test("part 1") {
    val input = """
        |value 5 goes to bot 2
        |bot 2 gives low to bot 1 and high to bot 0
        |value 3 goes to bot 1
        |bot 1 gives low to output 1 and high to bot 0
        |bot 0 gives low to output 2 and high to output 0
        |value 2 goes to bot 2
      """.stripMargin

    val solver = new Solver(input, Seq(5, 2))
    val actual = solver.solve
    assertResult(2)(actual)
  }
}
