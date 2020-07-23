import logging
from pyftpdlib.authorizers import DummyAuthorizer
from pyftpdlib.handlers import FTPHandler
from pyftpdlib.servers import FTPServer

host = "localhost"
port = 21
username = "testuser"
password = "testpass"
folder = "./testuser"

authorizer = DummyAuthorizer()
authorizer.add_user(username, password, folder, perm="elradfmw")

handler = FTPHandler
handler.authorizer = authorizer

#logging.basicConfig(filename='./pyftpd.log', level=logging.DEBUG)

server = FTPServer((host, port), handler)
server.max_cons = 300
server.max_cons_per_ip = 10

server.serve_forever()
