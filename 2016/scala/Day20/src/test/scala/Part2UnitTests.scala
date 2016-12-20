import org.scalatest._
import part2.Solver

import scala.collection.immutable.SortedSet

trait IpRangeLimited extends part2.Solver {
  override val max: Long = 10L
}

class Part2UnitTests extends FunSuite {
  test("part2") {
    val solver = new Solver(SortedSet[(Long, Long)]((5L,8L), (0L,2L), (4L,7L))) with IpRangeLimited
    val actual = solver.solve
    assertResult(3)(actual)
  }
}
