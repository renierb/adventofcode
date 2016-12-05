package part2

import java.security.MessageDigest
import scala.annotation.tailrec

class Solver(doorID: String) {

  val digest = MessageDigest.getInstance("MD5")

  val validIndexes = Array('0', '1', '2', '3', '4', '5', '6', '7')

  val emptyPassword = Array[Option[Char]](
    None, None, None, None,
    None, None, None, None)

  @tailrec
  final def solve(i: Int = 3231929, password: Array[Option[Char]] = emptyPassword): String = {
    if (password.forall(_.nonEmpty)) {
      password.map(_.get).mkString
    } else {
      val result = solveFor(i)
      if (result.isDefined) {
        val (pos, c) = result.get
        if (password(pos).isEmpty){
          solve(i + 1, password.updated(pos, Some(c)))
        } else {
          solve(i + 1, password)
        }
      } else {
        solve(i + 1, password)
      }
    }
  }

  def solveFor(i: Int): Option[(Int, Char)] = {
    val hash = MD5(s"$doorID$i")
    if (hash.startsWith("00000") && validIndexes.contains(hash(5)))
      Some((hash(5).toString.toInt, hash(6)))
    else
      None
  }

  def MD5(text: String): String = {
    digest.digest(text.getBytes).take(6).map("%02x".format(_)).mkString
  }
}
