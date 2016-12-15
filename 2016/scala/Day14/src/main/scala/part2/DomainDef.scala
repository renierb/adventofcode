package part2

import scala.annotation.tailrec

trait DomainDef extends part1.DomainDef {

  @tailrec
  final override protected def MD5(text: String, count: Int = 2016): String = {
    md5.Init()
    md5.Update(text)
    if (count == 0) {
      md5.asHex()
    } else {
      MD5(md5.asHex(), count - 1)
    }
  }
}
