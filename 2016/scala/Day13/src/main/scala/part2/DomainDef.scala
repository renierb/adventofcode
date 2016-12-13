package part2

/**
  * This trait represents the layout and building blocks of the maze
  */
trait DomainDef {

  /**
    * The case class `Pos` encodes positions in the terrain.
    *
    * IMPORTANT NOTE
    *  - The `x` coordinate denotes the position on the horizontal axis
    *  - The `y` coordinate is used for the vertical axis
    *  - The coordinates increase when moving down and right
    *
    * Illustration:
    *
    * 0 1 2 3   <- x axis
    * 0 . . . .
    * 1 . . . .
    * 2 . O . .    O is at position Pos(2, 1)
    * 3 . . . .
    *
    * |
    * y axis
    */
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
    * The target position where santa has to go.
    * This value is left abstract.
    */
  val goal: Pos

  /**
    * The terrain is represented as a function from positions to
    * booleans. The function returns `true` for every position that
    * is valid and inside the terrain.
    */
  type Terrain = Pos => Boolean

  /**
    * The terrain of this game. This value is left abstract.
    */
  val terrain: Terrain

  sealed abstract class Move
  case object Left extends Move
  case object Right extends Move
  case object Up extends Move
  case object Down extends Move

  /**
    * This function returns the santa at the start position of the game.
    */
  def startSanta: Block = Block(startPos)

  case class Block(pos: Pos) {

    def dx(d1: Int) = Block(pos.dx(d1))

    def dy(d1: Int) = Block(pos.dy(d1))

    def left: Block = dx(-1)

    def right: Block = dx(1)

    def up: Block = dy(-1)

    def down: Block = dy(1)

    /**
      * Returns the list of positions that can be obtained by moving
      * the current block where santa is located, together with the corresponding move.
      */
    def neighbors: List[(Block, Move)] = List(
      (left, Left),
      (right, Right),
      (up, Up),
      (down, Down)
    )

    /**
      * Returns the list of positions reachable from the current position
      * which are inside the terrain and not walls.
      */
    def legalNeighbors: List[(Block, Move)] =
      for (neighbor <- neighbors if neighbor._1.isLegal) yield neighbor

    /**
      * Returns `true` if this block is entirely inside the terrain.
      */
    def isLegal: Boolean = terrain(pos)
  }
}
