import org.scalatest._
import part1.InputParser

class Part1UnitTests extends FunSuite {
  test("part 1") {
    val input =
      """
        |The first floor contains a hydrogen-compatible microchip and a lithium-compatible microchip.
        |The second floor contains a hydrogen generator.
        |The third floor contains a lithium generator.
        |The fourth floor contains nothing relevant.
      """.stripMargin

    object Part1 extends part1.Solver {
      val floors = InputParser(input)
      override val startState = Elevator(0, floors.sortBy(_.nr).map(f => f.items.toSet).toArray)
    }

    val actual = Part1.solution.length
    assertResult(11)(actual)
  }
}
