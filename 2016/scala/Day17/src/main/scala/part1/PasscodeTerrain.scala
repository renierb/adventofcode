package part1

import java.security.MessageDigest

trait PasscodeTerrain extends DomainDef {
  private val digest = MessageDigest.getInstance("MD5")

  val passcode: String
  private val openCodes = Array('b', 'c', 'd', 'e', 'f')

  val terrain: Terrain = (pos: Pos, moves: Moves) => {
    val x = pos.x
    val y = pos.y

    x >= 0 && y >= 0 && x <= 3 && y <= 3 &&
      isOpen(moves.head, moves.tail.reverse)
  }

  def isOpen(move: Move, history: Moves): Boolean = {
    val hash = MD5(s"$passcode${history.mkString}")
    move match {
      case Up => openCodes.contains(hash(0))
      case Down => openCodes.contains(hash(1))
      case Left => openCodes.contains(hash(2))
      case Right => openCodes.contains(hash(3))
    }
  }

  def MD5(text: String): String = {
    digest.digest(text.getBytes).take(4).map("%02x".format(_)).mkString
  }
}