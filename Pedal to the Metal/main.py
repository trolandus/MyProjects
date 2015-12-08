from kivy.app import App
from kivy.core.window import Window
from kivy.properties import ObjectProperty
from kivy.uix.widget import Widget
from kivy.clock import Clock
from menu import Menu

class PedalGame(Widget):
    menu = ObjectProperty(None)

    def __init__(self, **kwargs):
        super(PedalGame, self).__init__(**kwargs)
        #self.m.showMenu()

class PedalApp(App):
    def build(self):
        game = PedalGame()
        # Clock.schedule_interval(game.update(), 1.0/60.0)
        return game

if __name__ == '__main__':
    PedalApp().run()
