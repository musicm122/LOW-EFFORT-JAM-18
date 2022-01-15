extends Node2D

signal player_interacting
signal player_interacting_complete

export var timeline = ""
# Declare member variables here. Examples:
# var a = 2
# var b = "text"
var interactAction = "shoot"
var canInteract = false

func waitSec(sec):
	var t = Timer.new()
	t.set_wait_time(sec)
	t.set_one_shot(true)
	self.add_child(t)
	t.start()
	yield(t, "timeout")
	t.queue_free()
	
#func _input(event):
#	if canInteract && !IsInteracting() && event.is_action_pressed(interactAction):
#		onInteract()
#		waitSec(3)
#		canInteract = true

func IsInteracting():
	return Input.is_action_just_pressed(interactAction)

func onInteract():	
	canInteract = false
	var dialog = Dialogic.start(timeline) 
	emit_signal("player_interacting")
	dialog.connect("dialogic_signal", self, "dialog_listener")
	add_child(dialog)
	waitSec(3)
	

func removeItem():
	get_tree().paused = false
	canInteract = false
	visible = false
	set_process(false)

func dialog_listener(arg):
	get_tree().paused = true
	print("dialog_listener called with", arg)
	
	match arg:
		"Flashlight":
			if name == "Item_Flashlight" :
				get_tree().call_group("Player", "AddFlashlight")
				removeItem()
		"Keys":
			if name == "Item_Keychain" :
				get_tree().call_group("Player", "AddKeys")
				get_tree().call_group("Level", "EndGame")
				removeItem()
				
		"start_dialog":
			if name == "Prisoner" :
				print("on_interact_complete called")
				emit_signal("player_interacting")
				canInteract = false
		"end_dialog":
			print("on_interact_complete called")
			emit_signal("player_interacting_complete")
			get_tree().paused = false
			yield(get_tree().create_timer(1.0), "timeout")
			canInteract = true
			
# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta):
	if canInteract && IsInteracting():
		onInteract()

func _on_Area2D2_body_entered(body):
	print("entered area")
	canInteract = body.get_name() == "Player"


func _on_Area2D2_body_exited(body):
	print("exited area")
	if body.get_name() == "Player": 
		canInteract = false
