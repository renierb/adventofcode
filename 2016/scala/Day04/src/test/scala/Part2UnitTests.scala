import org.scalatest._
import part2.Solver

class Part2UnitTests extends FunSuite {
  test("part 2") {
    val solver = new Solver(Stream(
      "qzmt-zixmtkozy-ivhz-343[zimth]"
    ))

    val actual = solver.solve("very")
    assertResult("very encrypted name")(actual.realName)
  }
}