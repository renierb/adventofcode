import org.scalatest._
import part1.Solver

class Part1UnitTests extends FunSuite {
  ignore("part 1") {
    val input =
      """
        |The first floor contains a hydrogen-compatible microchip and a lithium-compatible microchip.
        |The second floor contains a hydrogen generator.
        |The third floor contains a lithium generator.
        |The fourth floor contains nothing relevant.
      """.stripMargin

    val solver = new Solver(input)
    val actual = solver.solve
    assertResult(11)(actual)
  }

  test("moves HM from F1 to F2") {
    val state = List(("HG", 2), ("HM", 1), ("LG", 3), ("LM", 1))
    val actual = Solver.combinations(state, 1, 2)

    val expected = List(("HG", 2), ("HM", 2), ("LG", 3), ("LM", 1))
    assertResult(expected.sorted)(actual.head.sorted)
  }

  test("moves HG, HM from F2 to F3") {
    val state = List(("HG", 2), ("HM", 2), ("LG", 3), ("LM", 1))
    val actual = Solver.combinations(state, 1, 2)

    val expected = List(("HG", 3), ("HM", 3), ("LG", 3), ("LM", 2))
    assertResult(expected.sorted)(actual.head.sorted)
  }
}
