import org.scalatest._
import org.scalatest.prop.TableDrivenPropertyChecks

class Day01UnitTests extends FunSuite with TableDrivenPropertyChecks {
  val instructions =
    Table(
      ("instruction", "distance"),
      ("R2, L3", 5),
      ("R2, R2, R2", 2),
      ("R5, L5, R5, R3", 12)
    )

  test("instructions") {
    forAll (instructions) { (instructions, expected) =>
      val solver = new Day01(instructions.split(',').map(_.trim))
      val actual = solver.solve
      assertResult(expected)(actual)
    }
  }
}
