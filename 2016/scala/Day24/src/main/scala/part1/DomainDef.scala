package part1

trait DomainDef {

  case class Pos(x: Int, y: Int) {
    def dx(d: Int): Pos = copy(x = x + d)
    def dy(d: Int): Pos = copy(y = y + d)
  }

  type MapLayout = Vector[String]

  val maxX: Int
  val maxY: Int
  val maxLocations: Int

  val goal: Option[Pos] = None

  case class Map(pos: Pos, captured: Int, state: MapLayout) {

    private def dx(d1: Int) = move(pos.dx(d1))
    private def dy(d1: Int) = move(pos.dy(d1))

    private def left = if (pos.x > 1) dx(-1) else None
    private def right = if (pos.x < maxX) dx(1) else None
    private def up = if (pos.y > 1) dy(-1) else None
    private def down = if (pos.y < maxY) dy(1) else None

    private def move(to: Pos): Option[Map] = {
      val line = state(to.y)
      val next = line(to.x)
      if (next == '#') {
        None
      } else {
        if (('1' to '7').contains(next)) {
          val newState = state
            .updated(to.y, line.updated(to.x, '.'))
          Some(Map(to, captured + 1, newState))
        } else {
          Some(Map(to, captured, state))
        }
      }
    }

    def newStates: List[Map] =
      List(up, left, down, right).filter(_.isDefined).map(_.get)

    def isDone: Boolean =
      if (goal.isDefined)
        captured == maxLocations && goal.get == pos
      else
        captured == maxLocations
  }
}