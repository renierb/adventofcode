package part1

/**
  * This trait represents the problem domain
  */
trait DomainDef {
  /**
    * The case class `Pos` encodes positions in the cartesian plane.
    */
  case class Pos(x: Int, y: Int) {
    /** The position obtained by changing the `x` coordinate by `d` */
    def dx(d: Int) = copy(x = x + d)

    /** The position obtained by changing the `y` coordinate by `d` */
    def dy(d: Int) = copy(y = y + d)
  }

  sealed abstract class Direction
  case object North extends Direction
  case object South extends Direction
  case object East extends Direction
  case object West extends Direction

  case class Santa(direction: Direction, pos: Pos) {

    def dx(d: Direction, n: Int) = Santa(d, pos.dx(n))
    def dy(d: Direction, n: Int) = Santa(d, pos.dy(n))

    def move(instruction: String): Santa = instruction.head match {
      case 'L' => left(instruction.tail.toInt)
      case 'R' => right(instruction.tail.toInt)
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