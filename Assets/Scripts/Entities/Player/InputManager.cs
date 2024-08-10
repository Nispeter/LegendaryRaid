using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public delegate void InputAction(Vector2 input);
    public static event InputAction OnMove;
    public static event InputAction OnRun;
    public static event InputAction OnPause;
    public static event InputAction OnClick;
    public static event InputAction OnClickHold;
    public static event InputAction OnRelease;

    void Update()
    {
        Vector2 movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        OnMove?.Invoke(movementInput);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            OnRun?.Invoke(movementInput);
        }
        else
        {
            OnRun?.Invoke(Vector2.zero); 
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OnPause?.Invoke(Vector2.zero);
        }   
        if(SceneManager.Instance.CurrentGameState == GameState.Playing)
        {
            if(Input.GetMouseButtonDown(0))
            {
                OnClick?.Invoke(Input.mousePosition);
                //Debug.Log("Click");
            }
            if(Input.GetMouseButton(0))
            {
                OnClickHold?.Invoke(Input.mousePosition);
                //Debug.Log("Click Hold");
            }
            if (Input.GetMouseButtonUp(0))
            {
                OnRelease?.Invoke(Input.mousePosition);
                //Debug.Log("Release");
            }
        }
    }
}


// SINGLETON VS EVENT SYSTEM FOR INPUT CONTROLLER?
/*


I'll assume you're a seasoned developer, apologies if not. As always, the "correct" architecture depends on project complexity - an overarching singleton for direct calls is fine for small projects, but more flexible patterns can be better for more ambitious ones.

I can offer the following patterns, to be applied piecemeal or all together depending on your project requirements. Keep in mind that these are simply the solutions we found easiest and cleanest to code and maintain, not something deriving from a specific coding paradigm or philosophy.

    anything that keeps an internal state but doesn't have intelligence for changing that state by itself is a service. Communication is via Singleton, with direct methods calls returning either the desired result (sync) or a promise equivalent (async). Example: the pathfinder.

    entities with both an internal state and internal intelligence are managers. Communication is via event buses separated by domain concern, since no returned data is usually expected. Example: scene transition manager, reacting to events like player death or level end.

    UI stuff is usually closely matched with the Observer pattern, and as such can be cleanly implemented much like managers, with message bus subscription. That's how our UI reacts to player stat changes (score, hitpoints, etc.). However, polling a set player reference can work just as well.

Things to keep in mind:

    as I said before, split your message buses by domain. Each monster has its own bus with all its lifetime events (AI decisions, damage, etc.). The level manager has an event bus with level-related events (monster spawn, loot drops). Subscription can be reactive: the level manager reacts to a monster spawn by subscribing to the monster event bus, and unsubscribing when it dies.

    choosing between sync/async subscriptions can be tricky given the Unity workflow. If a death event is emitted in OnDestroy, you won't be able to access the gameobject for unsubscribing to its personal message bus, for example.

    when coding your message bus, be very careful about memory allocations. Using classes for events requires using pooling to avoid GC calls, and returning an event to the pool can be tricky if consumed by multiple async subscribers. I recommend struct events only.

    don't overdo it and turn the game into an event-fest. Many things can be handled with direct method calls which makes for far easier debugging. Use buses to avoid coupling on separate entities, not inside a given gameobject. A well-maintained list of monsters polled for their distance to the player can work just as well as a mess of OutOfPlayerRange events, and be far easier to maintain and debug.

As always, there is no hard and fast answers. Keep your scope in mind, avoid the code purist fanatics and refactor when you've found a choice isn't the right one anymore due to scope changes.

*/