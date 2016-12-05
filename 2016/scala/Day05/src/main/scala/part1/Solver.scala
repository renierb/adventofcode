package part1

import java.security.MessageDigest

class Solver(doorID: String) {

  val digest = MessageDigest.getInstance("MD5")

  def solve: String = {
    val chars = for {
      i <- (0 until Int.MaxValue).toStream
      c = solveFor(i)
      if c != ""
    } yield c

    chars.take(8).mkString
  }

  def solveFor(i: Int): String = {
    val md5 = MD5(s"$doorID$i")
    if (md5.startsWith("00000"))
      md5(5).toString
    else
      ""
  }

  def MD5(text: String): String = {
    digest.digest(text.getBytes).take(3).map("%02x".format(_)).mkString
  }
}
