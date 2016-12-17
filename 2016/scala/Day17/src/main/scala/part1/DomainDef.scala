package part1

/**
  * This trait represents the layout and building blocks of the maze
  */
trait DomainDef {

  case class Pos(x: Int, y: Int) {
    /** The position obtained by changing the `x` coordinate by `d` */
    def dx(d: Int): Pos = copy(x = x + d)

    /** The position obtained by changing the `y` coordinate by `d` */
    def dy(d: Int): Pos = copy(y = y + d)
  }

  /**
    * The position where Santa is located initially.
    * This value is left abstract.
    */
  val startPos: Pos

  /**
    * The target position where Santa has to go.
    * This value is left abstract.
    */
  val goal: Pos

  /**
    * The terrain is represented as a function from positions to
    * booleans. The function returns `true` for every position that
    * is valid and inside the terrain.
    */
  type Terrain = (Pos, Moves) => Boolean

  val terrain: Terrain

  sealed abstract class Move

  case object Left extends Move {
    override def toString: String = "L"
  }
  case object Right extends Move {
    override def toString: String = "R"
  }
  case object Up extends Move {
    override def toString: String = "U"
  }
  case object Down extends Move {
    override def toString: String = "D"
  }

  type Moves = List[Move]

  def startRoom: Room = Room(startPos, List())

  case class Room(pos: Pos, moves: List[Move]) {

    def dx(d1: Int, move: Move) = Room(pos.dx(d1), move :: moves)

    def dy(d1: Int, move: Move) = Room(pos.dy(d1), move :: moves)

    def left: Room = dx(-1, Left)

    def right: Room = dx(1, Right)

    def up: Room = dy(-1, Up)

    def down: Room = dy(1, Down)

    def neighbors: List[(Room, Move)] =
      if (pos == goal)
        List()
      else
        List(
          (up, Up),
          (down, Down),
          (left, Left),
          (right, Right))

    /**
      * Returns the list of positions reachable from the current room
      * which are inside the terrain and unlocked.
      */
    def legalNeighbors: List[(Room, Move)] =
      for (neighbor <- neighbors if neighbor._1.isLegal) yield neighbor

    def isLegal: Boolean = terrain(pos, moves)
  }
}