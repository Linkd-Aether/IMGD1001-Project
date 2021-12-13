using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostHome : GhostBehavior
{
    public Transform inside;
    public Transform outside;

    // Exit direction finding
    private List<Vector2> availableDirections;
    private LayerMask obstacleLayer;

    private void OnEnable()
    {
        StopAllCoroutines();
    }

    private void OnDisable()
    {
        // Check for active self to prevent error when object is destroyed
        if (this.gameObject.activeSelf) {
            StartCoroutine(ExitTransition());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Reverse direction everytime the ghost hits a wall to create the
        // effect of the ghost bouncing around the home
        if (this.enabled && collision.gameObject.layer == LayerMask.NameToLayer("Obstacles")) {
            this.ghost.movement.SetDirection(-this.ghost.movement.direction);
        }
    }

    private IEnumerator ExitTransition()
    {
        // Turn off movement while we manually animate the position
        this.ghost.movement.SetDirection(Vector2.up, true);
        this.ghost.movement.body.isKinematic = true;
        this.ghost.movement.enabled = false;

        Vector3 position = this.transform.position;

        float duration = 0.5f;
        float elapsed = 0.0f;

        // Animate to the starting point
        while (elapsed < duration)
        {
            this.ghost.SetPosition(Vector3.Lerp(position, this.inside.position, elapsed / duration));
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0.0f;

        // Animate exiting the ghost home
        while (elapsed < duration)
        {
            this.ghost.SetPosition(Vector3.Lerp(this.inside.position, this.outside.position, elapsed / duration));
            elapsed += Time.deltaTime;
            yield return null;
        }

        this.availableDirections = new List<Vector2>();
        this.obstacleLayer = LayerMask.GetMask("Obstacles");
        CheckAvailableDirection(Vector2.up);
        CheckAvailableDirection(Vector2.down);
        CheckAvailableDirection(Vector2.left);
        CheckAvailableDirection(Vector2.right);

        Vector2 direction = availableDirections[Random.Range(0,availableDirections.Count)];

        // Pick a random direction left or right and re-enable movement
        this.ghost.movement.SetDirection(direction, true);
        this.ghost.movement.body.isKinematic = false;
        this.ghost.movement.enabled = true;
    }

    private void CheckAvailableDirection(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.5f, 0.0f, direction, 1.0f, this.obstacleLayer);

        // If no collider is hit then there is no obstacle in that direction
        if (hit.collider == null) {
            this.availableDirections.Add(direction);
        }
    }

}
