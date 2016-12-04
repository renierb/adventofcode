package part2
import scala.collection.immutable.Seq

/**
  * This trait represents the problem domain
  */
trait DomainDef {
  /**
    * The case class `Pos` encodes positions in the cartesian plane.
    */
  case class Pos(x: Int, y: Int) {
    /** The position obtained by changing the `x` coordinate by `d` */
    def dx(d: Int): Pos = copy(x = x + d)

    /** The position obtained by changing the `y` coordinate by `d` */
    def dy(d: Int): Pos = copy(y = y + d)
  }

  sealed abstract class Direction
  case object North extends Direction
  case object South extends Direction
  case object East extends Direction
  case object West extends Direction

  case class Santa(direction: Direction, pos: Pos, history: Vector[Pos] = Vector(), hq: Option[Pos] = None) {

    private def matchExplored = new PartialFunction[Pos, Pos] {
      def apply(p: Pos): Pos = p
      def isDefinedAt(p: Pos): Boolean = history.contains(p)
    }

    def dx(d: Direction, n: Int): Santa = {
      lazy val explore: Seq[Pos] = {
        val step = if (n > 0) 1 else -1
        (pos.x until (pos.x + n) by step).map(x => Pos(x, pos.y))
      }

      val (hq1, history1) =
        if (hq.isEmpty)
          (explore.collectFirst(matchExplored), history ++ explore)
        else
          (hq, history) //Vector.empty[Pos] no need to keep the history if hq is known

      Santa(d, pos.dx(n), history1, hq1)
    }

    def dy(d: Direction, n: Int): Santa = {
      lazy val explore: Seq[Pos] = {
        val step = if (n > 0) 1 else -1
        (pos.y until (pos.y + n) by step).map(y => Pos(pos.x, y))
      }

      val (hq1, history1) =
        if (hq.isEmpty)
          (explore.collectFirst(matchExplored), history ++ explore)
        else
          (hq, history) //Vector.empty[Pos]

      Santa(d, pos.dy(n), history1, hq1)
    }

    def move(instruction: String): Santa = {
      instruction.head match {
        case 'L' => left(instruction.tail.toInt)
        case 'R' => right(instruction.tail.toInt)
      }
    }

    private def left(n: Int) = direction match {
      case North => dx(West, -n)
      case South => dx(East, n)
      case East => dy(North, n)
      case West => dy(South, -n)
    }

    private def right(n: Int) = direction match {
      case North => dx(East, n)
      case South => dx(West, -n)
      case East => dy(South, -n)
      case West => dy(North, n)
    }
  }
}