package part2

trait DomainDef {

  case class Pos(x: Int, y: Int) {
    def dx(d: Int): Pos = copy(x = x + d)
    def dy(d: Int): Pos = copy(y = y + d)
  }

  type Grid = Map[Pos, Char]

  val maxX: Int
  val maxY: Int

  case class StorageGrid(pos: Pos, state: Grid) {

    private def dx(d1: Int) = moveData(pos, pos.dx(d1))
    private def dy(d1: Int) = moveData(pos, pos.dy(d1))

    private def left = if (pos.x > 0) dx(-1) else None
    private def right = if (pos.x < maxX) dx(1) else None
    private def up = if (pos.y > 0) dy(-1) else None
    private def down = if (pos.y < maxY) dy(1) else None

    private def moveData(from: Pos, to: Pos): Option[StorageGrid] = {
      val dst = state(to)
      if (dst == '#') {
        None
      } else {
        val src = state(from)
        val newState = state
          .updated(to, src)
          .updated(from, dst)
        Some(StorageGrid(to, newState))
      }
    }

    def newStates: List[StorageGrid] =
      List(up, left, down, right).filter(_.isDefined).map(_.get)

    def isDone: Boolean = state(Pos(0, 0)) == 'G'
  }
}