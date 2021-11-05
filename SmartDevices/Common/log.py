import urequests

class Log:
    
    def __init__(self, url, source):
        self.__url = url
        self.__source = source
        
    def __get_message(self, message, detail):
        return '''{
                      "source": "''' + self.__source + '''",
                      "description": "''' + message + '''",
                      "detail": "''' + detail + '''"
                    }'''
    
    def error(self, message, detail):
        urequests.post(self.__url + '/error', data = self.__get_message(message, detail), headers = {'content-type': 'application/json'})
    
    def info(self, message, detail):
        response = urequests.post(self.__url + '/info', data = self.__get_message(message, detail), headers = {'content-type': 'application/json'})
        
    def warning(self, message, detail):
        urequests.post(self.__url + '/warning', data = self.__get_message(message, detail), headers = {'content-type': 'application/json'})
        
    def debug(self, message, detail):
        urequests.post(self.__url + '/debug', data = self.__get_message(message, detail), headers = {'content-type': 'application/json'})
        
        
    
    

